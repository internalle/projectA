var gulp = require("gulp");
var sass = require("gulp-sass");
var ts = require("gulp-typescript");
var imagemin = require("gulp-imagemin");

gulp.task("watch", ["build"], function(){
    gulp.watch('src/sass/*', ['build-sass']);
    gulp.watch('src/ts/*', ['build-typescript']);
    gulp.watch('src/images/*', ['minify-images']);
});

gulp.task("build", ["build-sass", "build-typescript", "minify-images"]);

gulp.task("build-sass", function(){ 
    gulp.src('src/sass/*')
   		.pipe(sass())
        .pipe(gulp.dest('dist/css/'));
});

gulp.task("build-typescript", function(){
    gulp.src('src/ts/*')
        .pipe(ts())
        .pipe(gulp.dest('dist/js'))
});

gulp.task("minify-images", function(){
    gulp.src('src/images/*')
        .pipe(imagemin())
        .pipe(gulp.dest('dist/images'))
});