var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serve static files and configure the app to use a default route
app.UseDefaultFiles();
app.UseStaticFiles();

// Handle the root route to display the HTML content
app.MapGet("/", () =>
{
    return Results.Content(@"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>My Web Page</title>
        <style>
            body {
                font-family: Arial, sans-serif;
                display: flex;
                justify-content: center;
                align-items: center;
                height: 100vh;
                margin: 0;
                background-color: #f0f0f0;
            }
            .container {
                text-align: center;
            }
            .btn {
                display: block;
                margin: 10px;
                padding: 10px 20px;
                font-size: 16px;
                cursor: pointer;
                background-color: #4CAF50;
                color: white;
                border: none;
                border-radius: 5px;
                transition: background-color 0.3s;
            }
            .btn:hover {
                background-color: #45a049;
            }
            #label {
                font-size: 18px;
                margin-bottom: 20px;
            }
        </style>
    </head>
    <body>
        <div class='container'>
            <div id='label'>This is a label</div>
            <button class='btn' onclick='buttonClick(1)'>Button 1</button>
            <button class='btn' onclick='buttonClick(2)'>Button 2</button>
            <button class='btn' onclick='buttonClick(3)'>Button 3</button>
        </div>

        <script>
            function buttonClick(btnId) {
                document.getElementById('label').innerText = 'You clicked Button ' + btnId;
            }
        </script>
    </body>
    </html>
    ", "text/html");
});

app.Run();
