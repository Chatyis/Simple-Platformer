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
