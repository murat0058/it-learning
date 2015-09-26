/* =========== Modules =========== */

var project = require('./project.json'),
    gulp = require('gulp'),
    concat = require('gulp-concat'),
    uglifyJs = require('gulp-uglify'),
    minifyCss = require('gulp-minify-css'),
    scss = require('gulp-sass');

/* =========== Variables & Helpers =========== */

var basePath = './' + project.webroot; 
    
var paths = {
    bower: basePath + '/src/_bower/',
    srcJs: basePath + '/src/js/',
    srcScss: basePath + '/src/scss/',
    srcCss: basePath + '/src/css/',
    distJs: basePath + '/dist/js/',
    distCss: basePath + '/dist/css/',
};

/* =========== Tasks - Developement =========== */

gulp.task('scss:compile', function () {

    return gulp.src(paths.srcScss + '*.scss')
        .pipe(scss().on('error', scss.logError))
        .pipe(gulp.dest(paths.srcCss));

});

/* =========== Tasks - Production =========== */

gulp.task('jquery:build', function () {

    var jqueryLibs = [
        paths.bower + 'jquery/dist/jquery.js',
        paths.bower + 'jquery-validation/dist/jquery.validate.js',
        paths.bower + 'jquery-validation-unobtrusive/jquery.validate.unobtrusive.js',
        paths.bower + 'jquery.scrollex/jquery.scrollex.js',
        paths.srcJs + 'landing/jquery.scrolly.min.js'
    ];

    return gulp.src(jqueryLibs)
        .pipe(concat('jquery-package.js'))
        .pipe(gulp.dest(paths.distJs))
        .pipe(uglifyJs())
        .pipe(concat('jquery-package.min.js'))
        .pipe(gulp.dest(paths.distJs));
});

/* =========== Tasks - Watch / Groups =========== */

gulp.task('scss:watch', function () {
    gulp.watch(paths.srcScss + '*.scss', ['scss:compile']);
});