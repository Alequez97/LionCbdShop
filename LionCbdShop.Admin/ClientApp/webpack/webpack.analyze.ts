import { BundleAnalyzerPlugin } from 'webpack-bundle-analyzer';
import StatoscopeWebpackPlugin from '@statoscope/webpack-plugin';

export default {
  plugins: [
    new BundleAnalyzerPlugin({ analyzerMode: 'static' }),
    new StatoscopeWebpackPlugin(),
  ],
};
