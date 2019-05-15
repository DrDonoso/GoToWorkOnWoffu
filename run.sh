#!/usr/bin/env bash
export $(cat secrets)
set -e

docker-compose rm -f
docker-compose build
docker-compose run gotoworkonwoffu