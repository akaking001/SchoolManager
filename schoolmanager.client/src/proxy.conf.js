const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "https://localhost:50443",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
