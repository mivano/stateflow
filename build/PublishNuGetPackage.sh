NUGET_PACKAGE_NUM=$BUILD_NUM
if [ "$TRAVIS_BRANCH" != "master" ]; then
	NUGET_PACKAGE_NUM+="-${TRAVIS_BRANCH}${TRAVIS_BUILD_NUMBER}"
fi
sed -i "s/<version>.*<\/version>/<version>${NUGET_PACKAGE_NUM}<\/version>/g" src/Stateflow/Stateflow.nuspec
mono --runtime=v4.0.30319 .nuget/NuGet.exe Pack src/Stateflow/Stateflow.nuspec -NonInteractive
mono --runtime=v4.0.30319 .nuget/NuGet.exe Push stateflow.${NUGET_PACKAGE_NUM}.nupkg $NUGET_APIKEY -NonInteractive