• How long did you spend on your solution?

- It took me about 3-days to complete both the backend and frontend.

• How do you build and run your solution?

- .Net 6 is needed
- On line: 26-> ToDoApp\Program.cs update the frontend url to reflect the port
  app.UseCors(opt => opt.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());

• What technical and functional assumptions did you make when implementing
your solution?

- Backend:
  Todo tasks are stored in memory for simplicity 

• Briefly explain your technical design and why do you think is the best
approach to this problem.

- Backend:    
  I followed SOLID principle for the implementation which result in loose coupling and high cohesion.
  For the API's I microservices approach with RestFul.
  I use fluent validation to value todo object.

- Frontend
  React app is use to create the FE Todo app, 
  I used React Redux toolkit query instead of vanilla react redux to handle the data follow of the.