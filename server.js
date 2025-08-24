#!/usr/bin/env node

import { StdioServerTransport } from "@modelcontextprotocol/sdk/server/stdio.js";
import { Server } from "@modelcontextprotocol/sdk/server/index.js";
import { CallToolRequestSchema, CallToolResultSchema } from "@modelcontextprotocol/sdk/types.js";

const server = new Server(
  {
    name: "demo-mcp-server",
    version: "1.0.0",
  },
  {
    capabilities: {
      tools: {},
    },
  }
);

// Simple tool: generateTestData
server.setRequestHandler(CallToolRequestSchema, async (request) => {
  if (request.params.name === "generateTestData") {
    return {
      content: [
        {
          type: "text",
          text: "TestUser_" + Math.floor(Math.random() * 1000),
        },
      ],
    };
  }

  throw new Error("Unknown tool: " + request.params.name);
});

const transport = new StdioServerTransport();
await server.connect(transport);
