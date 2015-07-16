/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    // load Grunt plugins from NPM
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-bowercopy');

    // configure plugins
    grunt.initConfig({
        bowercopy: {
            options: {
                srcPrefix: 'bower_components',
            },
            folders: {
                files: {
                    'wwwroot/bower_components/ng-grid': 'bower_components/ng-grid',
                    'wwwroot/bower_components/angular-signalr-hub': 'bower_components/angular-signalr-hub',
                    'wwwroot/bower_components/underscore' : 'bower_components/underscore'
                }
            }
        }
    });

    // define tasks
    grunt.registerTask('bower', ['bowercopy']);
};