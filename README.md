
# HackerNews API

This repository contains a simple C# API for retrieving data from HackerNews. It provides endpoints to fetch top best stories, and individual story details.

## Project Structure

The project is structured as a .NET 8.0 (or later) Web API. Here's a brief overview of the key components:

*   **Stories project:**  containing API controllers that handle incoming HTTP requests and return responses, this project is used as "Presentation Layer"
*   **Core project:**  is in charge to process implementing the logic to fetch data from the HackerNews API. Trasnfrom the data, defining the data structures used to represent HackerNews stories, this project is used as "Business Layer". 
*   **AppSettings project:**  Usually handles the appsettings.json configuration.

Core Project Notes: 
	- Im using MemoryCache in order to avoid send many request to: "https://hacker-news.firebaseio.com/v0/"
	- Im using Mapper in order to transform the original response from "https://hacker-news.firebaseio.com/v0/" to our expected object.
	- In the main response Im retunring the ID, this param was not required, but I´ve added because I did another endpoint that allow us make a query by Id

## API Endpoints

The API provides the following endpoints:


*   **GET api/HackerNews/beststories?NumberOfStories**:
    *   Retrieves the IDs of the best stories on HackerNews.
    *   Returns a List with the details as Json format, depending of this param:{NumberOfStories}  
*   **`GET /api/HackerNews/beststories/{id}`**:
    *   Retrieves the details of a specific HackerNews story by its ID.
    *   `id`: An integer representing the story ID.
    *   Returns a JSON object containing the story details (title, URL, author, score, etc.).  

## Getting Started

### Prerequisites

*   [.NET 8.0 (or later) SDK](https://dotnet.microsoft.com/en-us/download)
*   [Git](https://git-scm.com/)

### Installation

1.  **Clone the repository:**

    ```bash
    git clone https://github.com/dsejca/HackerNews.git
    cd HackerNews
    ```

2.  **Navigate to the project directory.** The exact path will depend on your solution structure (often a subdirectory within the cloned repository).  For example:

    ```bash
    cd HackerNews\src
    ```

  **Build the project:**

    ```bash
    HackerNews\src\dotnet build
    ```

  **Run the API:**

    ```bash
    HackerNews\src\cd Stories
    HackerNews\src\Stories\ dotnet run
    ```

    This will start the API, typically on `https://localhost:5174/swagger/index.html` or `https://localhost:5175/swagger/index.html`. The exact URL will be displayed in the console output.

3.  If you prefer use Visual Studio to compile and execute the solution, perfom below steps.
    
	- Navigate to the main directory: HackerNews\src\
	- double click on: HackerNews.sln
	- after open in your VS, restore the nuget package.
	- Build solution.
	- Execute the solution.

### Usage

Once the API is running, you can access the endpoints using any HTTP client (e.g., a web browser, `curl`, Postman, Insomnia).

**Example using a web browser:**

Simply enter the URL in your browser's address bar:

*   https://localhost:7251/api/HackerNews/beststories?numberOfStories=100
*   https://localhost:7251/api/HackerNews/beststories/43286161

### Configuration

The API's behavior can be configured through the `appsettings.json` file. You can adjust settings such as:

*   **BaseUrl:**  If the HackerNews API URL needs to be changed.
*   **MinutesForResetCache:** Configure caching strategies to improve performance.

### Dependencies

The project uses the following NuGet packages:

*  AutoMapper
*  Microsoft.Extensions.Caching.Abstractions
*  Microsoft.Extensions.Http
*  Microsoft.Extensions.Options
*  Newtonsoft.Json

### Swagger/OpenAPI

The API likely includes Swagger/OpenAPI documentation, which provides an interactive way to explore the API endpoints and test them directly in your browser.

To access the Swagger UI, navigate to `/swagger/index.html` in your browser (e.g., `http://localhost:5000/swagger/index.html`).

