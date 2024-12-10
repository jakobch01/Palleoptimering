#!/usr/bin/env bash
# Use this script to test if a given TCP host/port are available

WAITFORIT_cmdname=${0##*/}

echoerr() {
    if [[ $WAITFORIT_QUIET -ne 1 ]]; then echo "$@" 1>&2; fi
}

usage() {
    cat << USAGE >&2
Usage:
    $WAITFORIT_cmdname host:port [-s] [-t timeout] [-- command args]
    -h HOST:PORT  Specify the host and port to wait for
    -q            Quiet - don't output any status messages
    -s            Skip wait and just execute command
    -t TIMEOUT    Timeout in seconds, zero for no timeout (default: 15 seconds)
    -- COMMAND ARGS  Execute command with args after the test finishes
USAGE
    exit 1
}

wait_for() {
    if [[ $WAITFORIT_SKIP -eq 1 ]]; then
        return 0
    fi

    if [[ $WAITFORIT_TIMEOUT -gt 0 ]]; then
        echoerr "$WAITFORIT_cmdname: waiting $WAITFORIT_TIMEOUT seconds for $WAITFORIT_HOST:$WAITFORIT_PORT"
    else
        echoerr "$WAITFORIT_cmdname: waiting for $WAITFORIT_HOST:$WAITFORIT_PORT without a timeout"
    fi

    start_ts=$(date +%s)
    while :
    do
        if [[ $WAITFORIT_ISBUSY -eq 1 ]]; then
            nc -z $WAITFORIT_HOST $WAITFORIT_PORT
            result=$?
        else
            (echo > /dev/tcp/$WAITFORIT_HOST/$WAITFORIT_PORT) >/dev/null 2>&1
            result=$?
        fi
        if [[ $result -eq 0 ]]; then
            end_ts=$(date +%s)
            echoerr "$WAITFORIT_cmdname: $WAITFORIT_HOST:$WAITFORIT_PORT is available after $((end_ts - start_ts)) seconds"
            return 0
        fi
        sleep 1
    done
}

wait_for_wrapper() {
    # In order to support SIGINT during timeout: https://unix.stackexchange.com/a/57692
    if [[ $WAITFORIT_TIMEOUT -gt 0 ]]; then
        wait_for & PID=$!
        trap "kill -INT -$PID" INT
        wait $PID
        result=$?
        trap - INT
    else
        wait_for
        result=$?
    fi
    return $result
}

timeout=15
quiet=0
skip=0
isbusy=0

# process arguments
while [[ $# -gt 0 ]]
do
    case "$1" in
        *:* )
        WAITFORIT_HOST=$(printf "%s\n" "$1"| cut -d : -f 1)
        WAITFORIT_PORT=$(printf "%s\n" "$1"| cut -d : -f 2)
        shift 1
        ;;
        -q)
        quiet=1
        shift
        ;;
        -s)
        skip=1
        shift
        ;;
        -t)
        timeout="$2"
        if [[ $timeout = "" ]]; then break; fi
        shift 2
        ;;
        --)
        shift
        CLI="$@"
        break
        ;;
        *)
        usage
        ;;
    esac
done

if [[ "$WAITFORIT_HOST" = "" || "$WAITFORIT_PORT" = "" ]]; then
    echo "Error: you need to provide a host and port to test."
    usage
fi

WAITFORIT_QUIET=$quiet WAITFORIT_SKIP=$skip WAITFORIT_TIMEOUT=$timeout wait_for_wrapper

result=$?
if [[ $CLI != "" ]]; then
    if [[ $result -ne 0 ]]; then
        echoerr "$WAITFORIT_cmdname: timeout occurred after waiting $timeout seconds for $WAITFORIT_HOST:$WAITFORIT_PORT"
    fi
    exec $CLI
else
    exit $result
fi
