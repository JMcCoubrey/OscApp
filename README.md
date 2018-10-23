# OscApp
This is a generic project template which we can use to fork whenever we start new web application projects at EventMAP.  It features the Osc.Db submodule for the shared data set.

## Quick start
- Run `npm install` in **OscApp.Web**
- Run `npm start` in **OscApp.Web**
- Run the database migration; in the Package Manager Console run `Update-Database` with **Osc.Db** targetted
- Run `git submodule init` then `git submodule update` in repo's root
- Run `git checkout master` in **Osc.Db**
- Debug TimetablerTm.Web

## Contribution guidelines
See [the wiki page](https://github.com/Eventmap/TimetablerTm/wiki/General-coding-guidelines) for coding guidelines.
