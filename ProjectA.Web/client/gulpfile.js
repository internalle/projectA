var gulp = require("gulp");
var sass = require("gulp-sass");
var sassGlob = require('gulp-sass-glob');
var ts = require("gulp-typescript");
var imagemin = require("gulp-imagemin");
var tap = require("gulp-tap");
var fs = require("fs");

gulp.task("watch", ["build"], function(){
    gulp.watch('src/sass/**', ['build-sass']);
    gulp.watch('src/ts/**', ['build-typescript']);
    gulp.watch('src/images/**', ['minify-images']);
    gulp.watch('typings/**', ['build-referencesjs']);
});

gulp.task("build", ["build-sass", "build-typescript", "minify-images"]);

gulp.task("build-sass", function(){ 
    gulp.src('src/sass/style.scss')
   		.pipe(sassGlob())
   		.pipe(sass())
        .pipe(gulp.dest('dist/css/'));
});

gulp.task("build-typescript", ['build-referencesjs'], function(){
    gulp.src('src/ts/**/*.ts')
        .pipe(ts({
            out: 'compiled-ts.js',
            removeComments: false
        }))
        .pipe(gulp.dest('dist/js'))
});

gulp.task("build-referencesjs", function(){
	fs.writeFileSync("_references.ts", '/// <reference path="typings/tsd.d.ts" />\n\r');
	gulp.src('src/ts/**/*.ts')
		.pipe(tap(function(file, t) {
			var relPath = file.path.replace(file.base, "");
			var row = '/// <reference path="src/ts/' + relPath + '" />\n\r';
			row = row.replace("\\", "/");
  			fs.appendFileSync("_references.ts", row);
	    }));
});

gulp.task("minify-images", function(){
    gulp.src('src/images/*')
        .pipe(imagemin())
        .pipe(gulp.dest('dist/images'))
});