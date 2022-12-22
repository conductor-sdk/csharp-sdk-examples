FROM mcr.microsoft.com/dotnet/sdk:6.0 AS csharp
RUN mkdir /package
COPY /src/Examples /package/
WORKDIR /package

FROM csharp AS lint
RUN dotnet format --verify-no-changes *.csproj

FROM csharp AS run
ARG KEY
ARG SECRET
ARG CONDUCTOR_SERVER_URL
ENV KEY=${KEY}
ENV SECRET=${SECRET}
ENV CONDUCTOR_SERVER_URL=${CONDUCTOR_SERVER_URL}
RUN dotnet run
