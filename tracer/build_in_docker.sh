#!/usr/bin/env bash
set -euox pipefail

# in case we are being run from outside this directory
cd "$(dirname "$0")"

ROOT_DIR="$(dirname $(pwd))"
BUILD_DIR="$ROOT_DIR/tracer/build/_build"
IMAGE_NAME="signalfx-dotnet-tracing/debian-base"

docker build \
   --build-arg DOTNETSDK_VERSION=6.0.200 \
   --tag $IMAGE_NAME \
   --file "$BUILD_DIR/docker/debian.dockerfile" \
   "$BUILD_DIR"

docker run -it --rm \
    --mount type=bind,source="$ROOT_DIR",target=/project \
    --env NugetPackageDirectory=/project/packages \
    --env tracerHome=/project/tracer/bin/tracer-home \
    --env artifacts=/project/tracer/bin/artifacts \
    -p 5003:5003 \
    -v /ddlogs:/var/log/signalfx/dotnet \
    $IMAGE_NAME \
    dotnet /build/bin/Debug/_build.dll "$@"
