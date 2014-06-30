module.exports = function(grunt) {

    grunt.option('destFolder','build_artifacts');
    grunt.option('projectName','XO.BC.Publishing');
    grunt.option('target',grunt.option('target') || 'QA');
    grunt.option('buildConfig',grunt.option('buildConfig') || 'Debug');
    grunt.option('srcFolder', '../../Projects/'+grunt.option('projectName')+'/');
    grunt.option('srcTarget', '../../Projects/'+grunt.option('projectName')+'/bin/'+grunt.option('buildConfig')+'/');
    grunt.option('slnFile','../../Solutions/'+grunt.option('projectName')+'/'+grunt.option('projectName')+'.sln');
 	grunt.option('outputConfigFile',grunt.option('destFolder')+'/'+grunt.option('projectName')+'.dll.config');
    grunt.option('startTime',new Date());

    grunt.option( 'QA',{
		Connection_String:'Data Source=ITGLSSQL01;Initial Catalog=Partners;Integrated Security=SSPI',
		nsbFileShareDataBus:'D:\\\\NSBFileShareDataBus',
		Appender_File:'D:\\\\logs\\\\partners\\\\BC_Publishing.log',
        Journal_Appender_File:'D:\\\\logs\\\\partners\\\\journaling\\\\xo_bc.Publishing.Journal.log',
        Logging_Level:'ERROR',
        max_concurrency_level: 50
    });

    grunt.option( 'STAGE',{
		Connection_String:'Data Source=STGLSSQL01;Initial Catalog=Partners;Integrated Security=SSPI',
		nsbFileShareDataBus:'D:\\\\NSBFileShareDataBus',
		Appender_File:'D:\\\\logs\\\\partners\\\\BC_Publishing.log',
        Journal_Appender_File:'D:\\\\logs\\\\partners\\\\journaling\\\\xo_bc.Publishing.Journal.log',
        Logging_Level:'ERROR',
        max_concurrency_level: 50
    });

    grunt.option( 'PROD',{
		Connection_String:'Data Source=PRDLOCALCLSDB01.theknot.com;Initial Catalog=Partners;Integrated Security=SSPI',
		nsbFileShareDataBus:'D:\\\\NSBFileShareDataBus',
		Appender_File:'D:\\\\logs\\\\partners\\\\BC_Publishing.log',
        Journal_Appender_File:'D:\\\\logs\\\\partners\\\\journaling\\\\xo_bc.Publishing.Journal.log',
        Logging_Level:'ERROR',
        max_concurrency_level: 50
    });

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        destFolder: grunt.option('destFolder'),
		outputConfigFile: grunt.option('outputConfigFile'),

// tasks
        clean: {
            build: [grunt.option('destFolder')]
        },
        
		msbuild: {
            src: [grunt.option('slnFile')],

            options: {
                projectConfiguration: grunt.option('buildConfig'),
                platform: 'Any CPU',
                targets: ['Clean', 'Build'],
                version: 4.0,
                verbosity: 'quiet'
            }
        },
        
		xunit_runner:{
            run:{
                options: { xUnit : "xunit.console.clr4.exe", workingDir:"../../tools/xUnit" },
                files:{
                    dlls: ['../../Projects/XO.Local.Publishing.Test/bin/debug/XO.Local.Publishing.Test.dll']
                }
            }
        },

        copy: {
            release: {
                expand: true,
                deleteEmptyFolders:true,
                cwd:grunt.option('srcTarget'),
                src: ['**',
                    '!**/obj/**',
                    '!**/*.cs',
                    '!**/*.vb',
                    '!**/*.csproj',
                    '!**/*.csproj.*',
					'!App.config',
					'!App.config.hbs',
					'!*.dll.config',
					'!**/*.xml',
					'!**/*.pdb'
                ],
                dest: grunt.option('destFolder')
            }
        },

        hbsconfigpoke:{
            compile:{
                options:{
                    context:grunt.option(grunt.option('target'))
                },
                files:{
					'<%=outputConfigFile%>' : grunt.option('srcFolder') + '/App.config.hbs'
                }
            }
        },

        cleanempty: {
            options: {},
            src: [grunt.option('destFolder')+'/**']
        }
    });

    grunt.registerTask('logStart', 'start time and build params.', function() {
        grunt.log.writeln('grunt build started at: '+ grunt.option('startTime').toLocaleTimeString());
        grunt.log.writeln(grunt.option('destFolder'));
        grunt.log.writeln(grunt.option('projectName'));
        grunt.log.writeln(grunt.option('srcFolder'));
        grunt.log.writeln(grunt.option('srcTarget'));
        grunt.log.writeln(grunt.option('buildConfig'));
        grunt.log.writeln(grunt.option('target'));
        grunt.log.writeln(grunt.option('outputConfigFile'));
		grunt.log.writeln(grunt.option('slnFile'));
    });

    grunt.registerTask('logEnd', 'End time and build params.', function() {
        grunt.log.writeln('grunt build ended at: '+ new Date().toLocaleTimeString());
        var time = new Date(Math.abs(new Date() - grunt.option('startTime')));
        grunt.log.writeln('Duration: '+ ('0' + time.getMinutes()).slice(-2) +':'+ ('0' + time.getSeconds()).slice(-2));
    });

    grunt.registerTask('addOriginToGit', 'adding remote to branch if it doesnt exist.', function() {
        if (grunt.option('target').toLowerCase() != 'prod') { return; }
        var exec = require('child_process').exec;
        var done = grunt.task.current.async(); // Tells Grunt that an async task is complete
        exec('git ls-remote || git remote add origin git@git.xogrp.com:LocalPartners/Partners.git', function (err, stdout, stderr) {
            if (err) {
                grunt.fatal('Can not create the commit:\n  ' + stderr);
            }
            done(err);
        });
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-msbuild');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-cleanempty');
    grunt.loadNpmTasks('grunt-hbs-configpoke');
    grunt.loadNpmTasks('grunt-bump');
    grunt.loadNpmTasks('grunt-xunit-runner');

    grunt.registerTask('default', ['logStart', 'clean', 'msbuild',  'xunit_runner','copy', 'cleanempty', 'hbsconfigpoke', 'logEnd']);
    grunt.registerTask('deploy', ['logStart', 'clean', 'msbuild', 'xunit_runner', 'copy', 'cleanempty', 'hbsconfigpoke', 'logEnd']);
    grunt.registerTask('deploy_notests', ['logStart', 'clean', 'msbuild', 'copy', 'cleanempty', 'hbsconfigpoke', 'logEnd']);
    grunt.registerTask('deploy_prod', ['logStart', 'clean', 'addOriginToGit', 'msbuild', 'xunit_runner', 'copy', 'cleanempty', 'hbsconfigpoke', 'logEnd']);
};