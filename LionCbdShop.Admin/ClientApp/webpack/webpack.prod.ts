import { Configuration, DefinePlugin, ProgressPlugin } from 'webpack';

import { CleanWebpackPlugin } from 'clean-webpack-plugin';
import MiniCssExtractPlugin from 'mini-css-extract-plugin';
import CssMinimizerPlugin from 'css-minimizer-webpack-plugin';

const prodConfig: Configuration = {
  mode: 'production',
  devtool: 'source-map',
  plugins: [
    new DefinePlugin({
      'process.env.name': JSON.stringify('prod'),
    }),
    new ProgressPlugin(
      (percentage: number, message: string, ...args: string[]) =>
        console.info(`${Math.round(percentage * 100)}%`, message, ...args)
    ),
    new CleanWebpackPlugin(),
    new MiniCssExtractPlugin({
      filename: '[name].[contenthash].css',
    }),
    new CssMinimizerPlugin(),
  ],
  optimization: {
    realContentHash: true,
    splitChunks: {
      cacheGroups: {
        vendor: {
          test: /[\\/]node_modules[\\/]/,
          name: 'vendors',
          chunks: 'all',
        },
      },
    },
  },
};

export default prodConfig;
