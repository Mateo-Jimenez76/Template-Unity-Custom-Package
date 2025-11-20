# Template Repository For Custom Unity Packages


# Github Actions

This template repository includes actions to help automate workflows. All actions are disabled by default and will not run unless you reenable them.

| Action | Description | Runs When |
| ------ | ------------- | ------------ |
| Unity Package Unit Tests | Runs Unit tests on configured assemblies when c# files are changed within the repository. | Pull Requests to the main branch |
| Update Changelog | Automatically updates the CHANGELOG.md file based on keywords in commits. Runs on every commit to the main branch. | Commits to the main branch |

If you'd like to change when these actions run, you can refer to the official action documentation [here](https://docs.github.com/en/actions/how-tos/write-workflows/choose-when-workflows-run/trigger-a-workflow)
## Using The Changlog Action



## Using the Unit Tests Actions

### How it works

The action uses [tj-actions/changed-files](https://github.com/tj-actions/changed-files) to check for changed c# files. 
If c# files have been changed, then a cache check will happen for the TempProject/Library folder. After that the
[game-ci/unity-test-runner](https://github.com/game-ci/unity-test-runner) will run and return an artifact with the test results.

### Required Secrets

| Secret | Description |
| ------ | ----------- |
| UNITY_EMAIL | The email account associated with your Unity profile |
| UNITY_PASSWORD | The password associated with your Unity profile |
| UNITY_LICENSE | The license to use to activate the Unity application. See [game.ci/docs](https://game.ci/docs/github/activation) for more info | 
These secrets are required for the Temperary Unity project to run and therefor be used to perform the unit tests.

## Required Secrets

# Additional Reading

https://docs.unity3d.com/6000.2/Documentation/Manual/CustomPackages.html
