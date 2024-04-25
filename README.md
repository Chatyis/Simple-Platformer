# Simple Platformer
Is a game made using Godot engine with C# language. Core concept was to create a platformer focused on movement mechanics and avoiding damage

![2024-04-2521-41-01-ezgif com-video-to-gif-converter (1)](https://github.com/Chatyis/Simple-Platformer/assets/32648007/4263633a-39d2-46b1-a46a-c72b4fa82940)

![2024-04-2521-54-21-ezgif com-video-to-gif-converter (2)](https://github.com/Chatyis/Simple-Platformer/assets/32648007/8dce7289-f9a0-4566-892e-8fc023a86e0e)

![2024-04-2522-03-36-ezgif com-video-to-gif-converter](https://github.com/Chatyis/Simple-Platformer/assets/32648007/1b131462-5d7f-4083-8989-dce6d058a1f6)

# Developer notes

## Things to keep in mind:
* Before pushing your changes make sure not to push to the **main** branch!
* In order to keep commits connected to the issue, please keep to the conventional commits described in the Commits section
* When ticket is ready for a review reassign and add **code review** label
* Always merge with option **Squash and Merge**
* Remove branch from the remote after it is merged to the **main** branch

## Scripting and files

### Naming files and scripting:
* prefabs - snake_case
* nodes in scene, script names, method names (including signals)  - PascalCase
* variables - camelCase

## Issues

### Naming issues:
_&lt;type&gt;(&lt;optional_scope&gt;): &lt;description&gt; e.g:_

### Most common issue labels can be found in Issues -> Labels

## Branches

### Possible branch categories:
* feats - adding new functionality
* bugfixes - bugfixing of any type
* refactors - changing code structure without adding/removing any functionality

### Naming branches:
_&lt;category&gt;/&lt;issue-number&gt; e.g:_
* bugfixes/1
* feats/player-sliding

## Commits

### Possible issue types:
* feat
* fix
* refactor

### When commiting make sure You keep commit message to the Conventional committs:
_&lt;type&gt;(&lt;optional_scope&gt;): &lt;description&gt; #&lt;issue_number if exists&gt; e.g:_
* fix(PlayerMovement): changed Player movement acceleration #3 
* feat: added after jump particle system instantiation
