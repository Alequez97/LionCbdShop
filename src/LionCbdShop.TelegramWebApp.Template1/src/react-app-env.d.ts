/// <reference types="react-scripts" />

export {};

declare global {
  interface Window {
    Telegram: any;
  }
}

declare module "*.jpg"