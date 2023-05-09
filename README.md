# C# QuickStart Example
This repository contains sample applications that demonstrates the various features of Conductor C# SDK.

## SDK Features
C# SDK for Conductor allows you to:
1. Create workflow using Code
2. Execute workflows
3. Create workers for task execution and framework (TaskRunner) for executing workers and communicating with the server.
4. Support for all the APIs such as
    1. Managing tasks (poll, update etc.),
    2. Managing workflows (start, pause, resume, terminate, get status, search etc.)
    3. Create and update workflow and task metadata
    4. User and event management


### Running Example

> **Note**
Obtain KEY and SECRET from the playground or your Conductor server. [Quick tutorial for playground](https://orkes.io/content/docs/getting-started/concepts/access-control-applications#access-keys)

Export variables
```shell
export KEY=
export SECRET=
export CONDUCTOR_SERVER_URL=https://play.orkes.io/api
```

Run the main program
```shell
dotnet run
```

## Workflow
We create a simple 2-step workflow that fetches the user details and sends an email.

<table><tr><th>Visual</th><th>Code</th></tr>
<tr>
<td width="50%"><img src="workflow.png" width="250px"></td>
<td>
<pre> 
var getUserInfoTask = new SimpleTask("get_user_info", "get_user_info")
                    .WithInput("userId", "${workflow.input.userId}");
var emailOrSmsTask = new SwitchTask("emailorsms", "${workflow.input.notificationPref}")
        .WithDecisionCase(
            WorkflowInput.NotificationPreference.EMAIL.ToString(),
            new SimpleTask("send_email", "send_email")
                    .WithInput("email", "${get_user_info.output.email}")
        )
        .WithDecisionCase(
            WorkflowInput.NotificationPreference.SMS.ToString(),
            new SimpleTask("send_sms", "send_sms")
                    .WithInput("phoneNumber", "${get_user_info.output.phoneNumber}")
        );
return new ConductorWorkflow()
    .WithName("user_notification")
    .WithVersion(1)
    .WithInputParameter("userId")
    .WithInputParameter("notificationPref")
    .WithTask(getUserInfoTask, emailOrSmsTask);
</pre>
</td>
</tr>
</table>


## Worker
Workers are a simple interface implementation. See [GetUserInfo.cs](Examples/Worker/GetUserInfo.cs) for more details.

## Executing Workflows

There are two ways to execute a workflow:
1. Synchronously - useful for short duration workflows that completes within a few second.  
2. Asynchronously - workflows that runs for longer period

### Synchronous Workflow Execution

```csharp
WorkflowResourceApi#ExecuteWorkflow(...)
```

### Asynchronous Workflow Execution

```csharp
WorkflowResourceApi#StartWorkflow(...)
```

See [Main.cs](Examples/Main.cs) for complete code sample of workflow execution.
