#! /bin/sh

cd ._fake
./paket.sh restore
cd ..

./._fake/packages/FAKE/tools/FAKE.exe build.fsx $@
