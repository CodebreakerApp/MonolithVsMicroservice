﻿using CodeBreaker.Backend.Data.Models;

namespace CodeBreaker.Backend.SignalRHubs.Models;

public record class MoveMadePayload(int GameId, Move Move);