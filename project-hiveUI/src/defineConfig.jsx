export default defineConfig({
    server: {
      proxy: {
        "/api": "http://localhost:5183",
      },
    },
    plugins: [react()],
  })