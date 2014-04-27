NUGET_PACKAGE_NUM=$BUILD_NUM
if [ "$TRAVIS_BRANCH" != "master" ]; then
	NUGET_PACKAGE_NUM+="-${TRAVIS_BRANCH}${TRAVIS_BUILD_NUMBER}"
fi
sed -i "s/<version>.*<\/version>/<version>${NUGET_PACKAGE_NUM}<\/version>/g" Stateflow.nuspec
mono --runtime=v4.0.30319 src/.nuget/NuGet.exe Pack Stateflow.nuspec -NonInteractive
mono --runtime=v4.0.30319 src/.nuget/NuGet.exe Push Stateflow.${NUGET_PACKAGE_NUM}.nupkg $NUGET_APIKEY -NonInteractive