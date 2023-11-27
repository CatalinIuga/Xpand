/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        primary: "#a35762",
        "background-100": "#333333",
        "background-200": "#292929",
        "background-300": "#202020",
        text: "#f0f0f0",
      },
    },
  },
  plugins: [],
};
