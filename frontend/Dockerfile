# Stage 1: Build the React app
FROM node:18 AS build
WORKDIR /app

# Copy package files and install dependencies
COPY customers-app/package.json customers-app/package-lock.json ./
RUN npm install

# Copy the rest of the app and build it
COPY customers-app ./
RUN npm run build

# Stage 2: Serve with Nginx
FROM nginx:alpine
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
