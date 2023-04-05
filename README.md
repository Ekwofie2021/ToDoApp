• How long did you spend on your solution?

- It took me about 3-days to complete both the backend and frontend.

• How do you build and run your solution?

- .Net sdk is needed to build it
- On line: 26-> ToDoApp\Program.cs update the frontend url with ("http://localhost:3000") if you are running it on a different port.
  app.UseCors(opt => opt.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());

- Frontend:
  On line: 7 in ToDoAppUI\src\features\store.tsx, you need to update the backend url with baseQuery "https://localhost:7049/api/". 
  Ideally just change the port number: "https://localhost:{PORT-NUMBER}/api/"
  fetchBaseQuery({baseUrl: "https://localhost:7049/api/"}),

• What technical and functional assumptions did you make when implementing
your solution?

- Backend:
  Todo tasks object are stored in memory for simplicity 

• Briefly explain your technical design and why do you think is the best
approach to this problem.

- Backend:    
  I followed SOLID principle for the implementation which result in loose coupling and high cohesion.
  For the API's I followed microservices approach with RestFul.
  I use fluent validation to value todo object.
  When the API's were completed I use swagger and post-man to test them.

- Frontend
  React app is use to create the FE Todo app, 
  I used React Redux toolkit query instead of vanilla react redux to handle the data follow of the.

  I should I have improve the frontend validation to displace the error coming back from the backend to the user
  and added test
