importScripts("https://www.gstatic.com/firebasejs/9.1.3/firebase-app-compat.js");
importScripts("https://www.gstatic.com/firebasejs/9.1.3/firebase-messaging-compat.js");
firebase.initializeApp({
  apiKey: "AIzaSyA3IQFZ_Sf0z3ys9vauTrNQJUkkIcWLMRw",
  authDomain: "studentoglasi.firebaseapp.com",
  databaseURL: "https://studentoglasi-default-rtdb.europe-west1.firebasedatabase.app",
  projectId: "studentoglasi",
  storageBucket: "studentoglasi.appspot.com",
  messagingSenderId: "698711314435",
  appId: "1:698711314435:web:c2f9c131db674089887e36",
  measurementId: "G-GZ7C39E6XK"
});
const messaging = firebase.messaging();
