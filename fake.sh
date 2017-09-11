#! /bin/sh

./paket.sh restore --fail-on-checks

./packages/fake/tools/FAKE.exe build.fsx $@
