/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{razor, cshtml}"],
  theme: {
    extend: {
      backgroundImage: theme => ({
        'custom-bg': "url('/images/library-bg.jpg')",
    }),
      colors: {
        customWhite: "#F6F1F1",
        customBlue: "#2D4059",
        customRed: "#EA5455",
        customOrange: "#F07B3F",
        customYellow: "#FFD460"
      },
      fontFamily: {
        'rubik': ['Rubik', 'sans-serif']
      }
  },
  },
  plugins: []
}