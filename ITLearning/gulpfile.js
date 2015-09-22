/* --- Modules --- */

var project = require('./project.json'),
    gulp = require('gulp'),
    concat = require('gulp-concat'),
    uglify = require('gulp-uglify'),
    scss = require('gulp-sass');

/* --- Variables / Helpers --- */

var basePath = './' + project.webroot; 
    
var paths = {
    lib: basePath + '/app/src/lib/',
    js: basePath + '/app/src/js/',
    dist: basePath + '/app/dist/',
    scss: basePath + '/scss/',
    css: basePath + '/css/'
};


/* --- Tasks --- */
gulp.task('jquery-build', function () {

    var jqueryLibs = [
        paths.lib + 'jquery/dist/jquery.js',
        paths.lib + 'jquery-validation/dist/jquery.validate.js',
        paths.lib + 'jquery-validation-unobtrusive/jquery.validate.unobtrusive.js',
        paths.lib + 'jquery.scrollex/jquery.scrollex.js',
        paths.js + 'landing/jquery.scrolly.min.js'
    ];

    return gulp.src(jqueryLibs)
        .pipe(concat('jquery-package.js'))
        .pipe(gulp.dest(paths.dist))
        .pipe(uglify())
        .pipe(concat('jquery-package.min.js'))
        .pipe(gulp.dest(paths.dist));
});

gulp.task('scss-compile', function () {

    return gulp.src(paths.scss + '*.scss')
        .pipe(scss().on('error', scss.logError))
        .pipe(gulp.dest(paths.css));
        
});
