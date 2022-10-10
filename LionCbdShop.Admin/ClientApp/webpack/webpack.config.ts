import { Configuration } from 'webpack';
import { merge } from 'webpack-merge';

import commonConfig from './webpack.common';
import devConfig from './webpack.dev';
import prodConfig from './webpack.prod';
import analyzeConfig from './webpack.analyze';

const { env, analyze } = process.env;
const envConfig = env === 'dev' ? devConfig : prodConfig;
const webpackConfig: Configuration = analyze
  ? merge(commonConfig, envConfig, analyzeConfig)
  : merge(commonConfig, envConfig);

export default webpackConfig;
