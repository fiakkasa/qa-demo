Parser.Default.ParseArguments<CliOptions>(args)
    .WithParsed(options =>
    {
        using var httpClient = new HttpClient();

        var scenarios = new[]
        {
            Scenario.Create(
                "index_page_responds_scenario",
                async context => await httpClient.GetAsync(options.Url) switch
                {
                    { IsSuccessStatusCode: true } => Response.Ok(),
                    _ => Response.Fail()
                }
            ),
            Scenario.Create(
                "privacy_page_responds_scenario",
                async context => await httpClient.GetAsync(options.Url) switch
                {
                    { IsSuccessStatusCode: true } => Response.Ok(),
                    _ => Response.Fail()
                }
            )
        }
        .Select(scenario =>
            scenario
                .WithoutWarmUp()
                .WithLoadSimulations(
                    Simulation.Inject(
                        rate: 10,
                        interval: TimeSpan.FromSeconds(1),
                        during: TimeSpan.FromSeconds(30)
                    )
                )
        );

        NBomberRunner
            .RegisterScenarios(scenarios.ToArray())
            .WithTestSuite("QA Demo Test Suite")
            .WithTestName("Index Page Load Tests")
            .WithReportFolder(Path.Combine(options.ReportDir))
            .Run();
    });