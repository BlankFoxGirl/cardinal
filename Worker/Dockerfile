FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
# .NET globalization APIs will use invariant mode by default because DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true is set
# by the base runtime-deps image. See https://aka.ms/dotnet-globalization-alpine-containers for more information.

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0.5-alpine3.15-amd64
# .NET Runtime version
ENV DOTNET_VERSION=6.0.5

# Install .NET Runtime
RUN wget -O dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Runtime/$DOTNET_VERSION/dotnet-runtime-$DOTNET_VERSION-linux-musl-x64.tar.gz \
    && dotnet_sha512='de0224c5cb933ff557d19c4293a7a3591a54ae1b5d2de1f663195a1cab34c89986999fd63d43fe6d31fc5ad467d5f5cbd15636fa672c34303fc7eddb1708db7f' \
    && echo "$dotnet_sha512  dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -oxzf dotnet.tar.gz -C /usr/share/dotnet \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

WORKDIR /app
COPY --from=build-env /app/out .

CMD ["dotnet", "Worker.dll"]