const path = require("path");
const TerserPlugin = require("terser-webpack-plugin");

module.exports = {
    entry: {
        scroll: '../wwwroot/scroll.js',
    },
    output: {
        path: path.resolve(__dirname, '../wwwroot/js'),
        filename: "[name].min.js",
        library: 'scroll',
    }
};