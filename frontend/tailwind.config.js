/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
      './src/**/*.{js,ts,jsx,tsx}',
    ],
    theme: {
      extend: {
        fontFamily: {
            sans: ['Roboto', '-apple-system', 'BlinkMacSystemFont', 'Segoe UI', 'Helvetica Neue', 'Arial', 'sans-serif'],
        },
        colors: {
          primary: '#FF69B4',
          foreground: '#ffffff',
          primaryLight: '#FFD6E8',
          darkText: '#171717',
          lightText: '#ededed',
          background: '#1f2937',
          border: '#f9a8d4',
          accent: '#FF85A1',
          muted: '#9ca3af',
        },
      },
    },
    plugins: [],
  }
  