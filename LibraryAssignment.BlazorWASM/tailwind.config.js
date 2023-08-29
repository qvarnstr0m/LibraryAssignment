/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{razor, cshtml}"],
  theme: {
    extend: {
      backgroundImage: theme => ({
        'custom-bg': "url('/images/library-bg.jpg')",
    }),
      colors: {
        customWhite: "#F8F3D4",
        customTeal: "#00B8A9",
        customPink: "#F6416C",
        customYellow: "#FFDE7D",
        customBlack: "#303841"
      },
      fontFamily: {
        'standard': ['Rubik', 'sans-serif']
      }
  },
  },
  plugins: []
}