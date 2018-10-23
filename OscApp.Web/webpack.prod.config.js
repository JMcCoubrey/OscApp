const webpack = require('webpack');
const AssetsPlugin = require('assets-webpack-plugin');
const path = require('path');

var BUILD_DIRECTORY = '/dist/';
var BUILD_DROP_PATH = path.join(__dirname, '/wwwroot/' + BUILD_DIRECTORY);

module.exports = {
    entry: {
        common: "./wwwroot/js/common.js",
		account: "./wwwroot/js/account/index.js",
        home: "./wwwroot/js/home/index.js",
        staff: './wwwroot/js/crud/staff/mainview.js',
        login: "./wwwroot/js/login/index.js"
    },
    output: {
    path: BUILD_DROP_PATH,
    publicPath: BUILD_DIRECTORY,
    filename: '[name].[hash].js'
  },
    resolve: {
        extensions: ['.js', '.html'],
        alias: {
            "bootstrap-datetimepicker$": "eonasdan-bootstrap-datetimepicker",
            "jquery-ui": "jquery-ui-dist/jquery-ui.js",
            "handlebars": 'handlebars/runtime.js',
            modules: path.join(__dirname, "node_modules")
        }
    },
    plugins: [
        new webpack.ProvidePlugin({
          $: "jquery",
          jQuery: "jquery",
          "window.jQuery": "jquery",
          _: "lodash-amd"
        }),
        new AssetsPlugin({
            filename: 'webpack.assets.json',
            path: __dirname,
            prettyPrint: true
        })
    ],
    module: {
        rules: [
            { test: /\.js$/, loader: 'babel-loader', exclude: /node_modules/, include: __dirname },
            { test: /\.css$/, loader: "style-loader!css-loader" },
            { test: /\.scss$/, loader: "style-loader!css-loader!sass-loader" },
            { test: /\.(jpg|gif|png)$/, loader: 'url-loader?limit=1000', include: __dirname },
            { test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/, loader: "url-loader?limit=10000&mimetype=application/font-woff" },
            { test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?$/, loader: "file-loader" },
            { test: /\.hbs?$/, loaders: ['handlebars-loader'], include: __dirname }
        ]
    }
};