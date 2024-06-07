import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./components/**/*.{js,ts,jsx,tsx,mdx}",
    "./app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      backgroundImage: {
        "gradient-radial": "radial-gradient(var(--tw-gradient-stops))",
        "gradient-conic":
          "conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))",
      },
      colors: {
        primary: 'rgba(0, 90, 161, 1)', // Primary color
        secondary: 'rgba(227, 227, 227, 1)', // Secondary color
        background: 'rgba(238, 242, 245, 1)', // Background color
        'footer-background': 'rgba(55, 55, 55, 1)', // Footer background color
        "white-background": 'rgba(255, 255, 255, 1)', // Foreground color
        'text-on-navbar': 'rgba(66, 66, 66, 1)', // Text color on navbar
        'text-on-footer-dark': 'rgba(124, 124, 124, 1)', // Text color on footer (dark)
        'text-on-footer-light': 'rgba(224, 224, 224, 1)', // Text color on footer (light)
        'text-on-white-light': 'rgba(126, 126, 126, 1)', // Text color on white background (light)
        'text-on-white-dark': 'rgba(0, 0, 0, 1)', // Text color on white background (dark)
      }
    },
  },
  plugins: [],
};
export default config;
