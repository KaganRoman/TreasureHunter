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
        uglify: {
            my_target: {
                files: { 'wwwroot/app.js': ['Scripts/app.js', 'Scripts/**/*.js'] }
            }
        },

        watch: {
            scripts: {
                files: ['Scripts/**/*.js'],
                tasks: ['uglify']
            }
        },

        bowercopy: {
            options: {
                srcPrefix: 'bower_components',
            },
            scripts: {
                options: {
                    destPrefix: 'wwwroot/bower_components'
                },
                files: {
                    "ace-builds/src-min-noconflict/ace.js": "ace-builds/src-min-noconflict/ace.js",
                    "angular-ui-ace/ui-ace.js": "angular-ui-ace/ui-ace.js",
                    "angular-signalr-hub/signalr-hub.min.js": "angular-signalr-hub/signalr-hub.min.js",
                    "jquery/jquery.min.js": "jquery/dist/jquery.min.js",
                }
            }
        }
    });

    // define tasks
    grunt.registerTask('default', ['uglify', 'watch']);
    grunt.registerTask('bower', ['bowercopy']);
};