/// <binding ProjectOpened='scss:watchBaseScss, scss:watchDirectivesScss' />
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
    bower: basePath + '/lib/',
    srcJs: basePath + '/src/js/',
    srcBaseScss: basePath + '/src/scss/',
    srcDirectivesScss: basePath + '/src/scss/directives/',
    srcBaseCss: basePath + '/src/css/',
    srcDirectivesCss: basePath + '/src/css/directives/',
    distJs: basePath + '/dist/js/',
    distCss: basePath + '/dist/css/',
};

/* =========== Tasks - Js =========== */

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

/* =========== Tasks - Scss =========== */

gulp.task('scss:compileBaseScss', function () {

    return gulp.src(paths.srcBaseScss + '*.scss')
        .pipe(scss().on('error', scss.logError))
        .pipe(gulp.dest(paths.srcBaseCss));

});

gulp.task('scss:compileDirectivesScss', function () {

    return gulp.src(paths.srcDirectivesScss + '*.scss')
        .pipe(scss().on('error', scss.logError))
        .pipe(gulp.dest(paths.srcDirectivesCss));

});

gulp.task('scss:watchBaseScss', function () {
    gulp.watch(paths.srcBaseScss + '*.scss', ['scss:compileBaseScss']);
});

gulp.task('scss:watchDirectivesScss', function () {
    gulp.watch(paths.srcDirectivesScss + '*.scss', ['scss:compileDirectivesScss']);
});