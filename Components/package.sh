#!/bin/sh

VERSION=$1
if [ -z "$VERSION" ]; then
    echo "Must specify version."
    exit 1
fi

zip -j LiveSplit.Skyrim_v${VERSION}.zip LiveSplit.Skyrim.dll ../README.md
