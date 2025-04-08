FROM node:alpine
WORKDIR /app

EXPOSE 3000

# Copy package files and install dependencies
COPY customers-app/package*.json ./
RUN npm install

# Copy the rest of the app and build it
COPY customers-app ./
CMD ["npm", "start"]