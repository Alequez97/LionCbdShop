// const telegramWebApp = window.Telegram.WebApp;
// telegramWebApp.ready();

// telegramWebApp.offEvent = function(eventType, callback) {
//   if (eventHandlers[eventType] === undefined) {
//     return;
//   }

//   var index = -1;
//   for (let i = 0; i < eventHandlers[eventType].length; i++) {
//     const callbackFromArray = eventHandlers[eventType][i];
//     if (callbackFromArray.name === callback.name) {
//       index = i;
//     }
//   }

//   if (index === -1) {
//     return;
//   }

//   eventHandlers[eventType].splice(index, 1);
// };