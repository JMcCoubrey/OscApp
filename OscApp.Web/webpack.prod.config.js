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
        coursegrid: "./wwwroot/js/gridview/coursegridview.js",
        buildings: './wwwroot/js/crud/building/mainview.js',
        coursetypes: './wwwroot/js/crud/coursetype/mainview.js',
        roomtypes: './wwwroot/js/crud/roomtype/mainview.js',
        stafftypes: './wwwroot/js/crud/stafftype/mainview.js',
        sites: './wwwroot/js/crud/site/mainview.js',
        departments: './wwwroot/js/crud/department/mainview.js',
        shifts: './wwwroot/js/crud/shift/mainview.js',
        teams: './wwwroot/js/crud/team/mainview.js',
        staff: './wwwroot/js/crud/staff/mainview.js',
        rooms: './wwwroot/js/crud/room/mainview.js',
        modules: './wwwroot/js/crud/module/mainview.js',
        roomgrid: "./wwwroot/js/gridview/roomgridview.js",
		staffgrid: "./wwwroot/js/gridview/staffgridview.js",
		settings: "./wwwroot/js/settings/index.js",
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