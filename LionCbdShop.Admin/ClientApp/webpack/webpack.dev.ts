import {
  DefinePlugin,
  WatchIgnorePlugin,
  Configuration,
  ContextReplacementPlugin,
} from 'webpack';
import ReactRefreshWebpackPlugin from '@pmmmwh/react-refresh-webpack-plugin';
import {
  Configuration as DevServerConfiguration,
  ProxyConfigArray,
} from 'webpack-dev-server';

const {
  env: { ASPNETCORE_HTTPS_PORT, ASPNETCORE_URLS },
} = process;

// Proxy target to C# dev server
// Port env variables set by running project in VS
// Fallback 5001 used when running C# server from command-line
const target = ASPNETCORE_HTTPS_PORT
  ? `https://localhost:${ASPNETCORE_HTTPS_PORT}`
  : ASPNETCORE_URLS?.split(';')[0] || 'https://localhost:5001';

// Workaround to circumvent typing bug
// where "context" is not a valid "proxy" option
const proxy = [
  {
    context: ['/auth', '/signin-oidc', '/api', '/v3'],
    target,
    secure: false,
  },
] as unknown as ProxyConfigArray;

const devConfig: Configuration & { devServer: DevServerConfiguration } = {
  mode: 'development',
  devServer: {
    server: 'https',
    historyApiFallback: true,
    hot: true,
    port: 44378,
    proxy,
  },
  target: 'web',
  devtool: 'eval-nosources-cheap-module-source-map',
  plugins: [
    new DefinePlugin({
      'process.env.name': JSON.stringify('dev'),
    }),
    new ReactRefreshWebpackPlugin(),
    new WatchIgnorePlugin({ paths: [/scss\.d\.ts$/, /\.test\.tsx?$/] }),
    new ContextReplacementPlugin(/power-assert-formatter/),
  ],
  stats: 'errors-only',
};

export default devConfig;
