importScripts("https://www.gstatic.com/firebasejs/9.1.3/firebase-app-compat.js");
importScripts("https://www.gstatic.com/firebasejs/9.1.3/firebase-messaging-compat.js");
firebase.initializeApp({
  apiKey: "AIzaSyAzBKPkK1jUQUpSsk-QjAw3AgOXR0rczkU",
  authDomain: "rs1app.firebaseapp.com",
  databaseURL: "https://rs1app-default-rtdb.europe-west1.firebasedatabase.app",
  projectId: "rs1app",
  storageBucket: "rs1app.appspot.com",
  messagingSenderId: "84477251666",
  appId: "1:84477251666:web:0875221eb02ea0eea40f8b",
  measurementId: "G-315JMFY2ZG"
});
const messaging = firebase.messaging();
