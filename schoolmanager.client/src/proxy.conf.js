const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target: "https://localhost:50443",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
