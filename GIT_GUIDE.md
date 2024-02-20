# Git Guide

## Useful Commands

* `git pull`    - Fetch and merge any commits from the tracking remote branch into the local branch
* `git add [file]`  - Adds all modified and new (untracked) files in the current directory and all subdirectories to the staging area, thus preparing them to be included in the next git commit.
* `git commit -m "[message]"` - Commit your staged content as a new commit snapshot
* `git push`    - Updates the remote repository branch with the files specified in the commit
* `git switch -c [branch_name]` - Create and switch to a new branch from your active branch 
* `git switch [branch_name]`    - Switch to another branch
* `git branch -d [branch_name]` - Delete the local branch 
* `git log` - Show the commit history for the currently active branch

## Git Commit Conventions
Check out [this linked article](https://www.conventionalcommits.org/en/v1.0.0/) for more examples on conventional commits.

### Types
Must be one of the following:

* **build**: Changes that affect the build system or external dependencies (example scopes: gulp, broccoli, npm)
* **ci**: Changes to our CI configuration files and scripts (example scopes: Travis, Circle, BrowserStack, SauceLabs)
* **docs**: Documentation only changes
* **feat**: A new feature
* **fix**: A bug fix
* **perf**: A code change that improves performance
* **refactor**: A code change that neither fixes a bug nor adds a feature
* **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
* **test**: Adding missing tests or correcting existing tests

### Quick Example Commit
* **Template** `git commit -m "[type]: [short description of what you did]"`

* **Example** `git commit -m "feat: add hp to boss character"`


## Branch Naming Conventions
Check out the basic rules section in [this linked article](https://medium.com/@abhay.pixolo/naming-conventions-for-git-branches-a-cheatsheet-8549feca2534) for more details on branch naming conventions.

### Quick Example New Branch
* **Template** `git switch -c "[type]/[short-description-of-feature]"`

*  **Example** `git switch -c "feature/add-boss-character"`