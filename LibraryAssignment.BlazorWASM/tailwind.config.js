/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{razor, cshtml}"],
  theme: {
    extend: {
      backgroundImage: theme => ({
        'custom-bg': "url('/images/library-bg.jpg')",
    }),
      fontFamily: {
        'english': ['Uncial Antiqua', 'cursive']
      }
  },
  }, 
  plugins: []
}