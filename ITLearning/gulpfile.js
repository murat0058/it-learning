var project = require('./project.json'),
    gulp = require('gulp'),
    concat = require('gulp-concat'),
    sass = require('gulp-sass');

var basePath = './' + project.webroot; 
    
var paths = {
    lib: basePath + '/app/src/lib/',
    dist: basePath + '/app/dist/'
};

gulp.task('jquery-build', function () {

    var jqueryLibs = [
        paths.lib + 'jquery/dist/jquery.js',
        paths.lib + 'jquery-validation/dist/jquery.validate.js',
        paths.lib + 'jquery-validation-unobtrusive/jquery.validate.unobtrusive.js',
        paths.lib + 'jquery-scrolly/jquery.scrolly.js',
        paths.lib + 'jquery.scrollex/jquery.scrollex.js'
    ];

    return gulp.src(jqueryLibs)
        .pipe(concat('jquery-package.js'))
        .pipe(gulp.dest(paths.dist));
});

gulp.task('jquery-min-build', function () {

    var jqueryLibs = [
        paths.lib + 'jquery/dist/jquery.min.js',
        paths.lib + 'jquery-validation/dist/jquery.validate.min.js',
        paths.lib + 'jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js',
        paths.lib + 'jquery-scrolly/jquery.scrolly.js',
        paths.lib + 'jquery.scrollex/jquery.scrollex.min.js'
    ];

    return gulp.src(jqueryLibs)
        .pipe(concat('jquery-package.min.js'))
        .pipe(gulp.dest(paths.dist));
});

gulp.task('default', [
    'jquery-build',
    'jquery-min-build'
]);