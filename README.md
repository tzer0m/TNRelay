# TNRelay

[![Deploy](https://github.com/tzer0m/TNRelay/actions/workflows/deploy.yml/badge.svg)](https://github.com/tzer0m/TNRelay/actions/workflows/deploy.yml)

A small console app that forwards [TrueNAS](https://www.truenas.com/) alerts as push notifications, since TrueNAS SCALE has no first-class way to relay alerts to a custom notification service.

## What it does

On each run, TNRelay:

1. Connects to one or more configured TrueNAS instances over a JSON-RPC-over-WebSocket API (`auth.login_with_api_key`, then `alert.list`).
2. Filters out dismissed alerts and anything below a configured minimum severity (`Info` → `Emergency`).
3. Checks a Postgres-backed dedupe store so an alert already forwarded isn't sent again.
4. Sends new alerts as push notifications via the [`t0m.Ting`](https://www.nuget.org/packages/t0m.Ting) client.
5. Marks each forwarded alert in the dedupe store.

It exits with the number of source errors encountered, so it's straightforward to alert on failures when run on a schedule.

## Tech stack

- .NET 10 console app
- Raw `ClientWebSocket` + JSON-RPC for the TrueNAS middleware API (no official .NET client exists)
- Npgsql for the dedupe store
- `t0m.Ting` for push delivery

## Configuration

Configuration lives in `appsettings.json` (see `appsettingsGit.json` for the shape, values stripped):

```json
{
  "TrueNas": {
    "Sources": [
      { "Name": "", "BaseUrl": "", "ApiKey": "" },
      { "Name": "", "BaseUrl": "", "ApiKey": "" }
    ],
    "MinSeverity": "Critical"
  },
  "Postgres": {
    "ConnectionString": ""
  },
  "Relay": {
    "Endpoint": ""
  }
}
```

Each entry in `TrueNas:Sources` is one TrueNAS instance to poll, identified by a `Name` that's also used as the dedupe key prefix.

## Deployment

Deployed via GitHub Actions on push to `master`, using a self-hosted runner, which publishes the build. TNRelay runs to completion and exits rather than staying resident, so it's intended to be invoked periodically (e.g. via cron or a systemd timer) rather than run as a long-lived service.
