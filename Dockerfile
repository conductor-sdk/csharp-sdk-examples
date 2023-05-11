FROM mcr.microsoft.com/dotnet/sdk:6.0 AS csharp
RUN mkdir /package
COPY /Examples /package/
WORKDIR /package

RUN dotnet format --verify-no-changes *.csproj

ENV KEY=$KEY
ENV SECRET=$SECRET
ENV CONDUCTOR_SERVER_URL=$CONDUCTOR_SERVER_URL

CMD ["dotnet", "run"]
