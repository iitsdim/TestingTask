using Contracts.Observation;
using Domain.Data;
using Domain.Entities;
using Domain.Models;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
namespace Domain.Services.Sequence;

public class SequenceService : ISequenceService 
{
    private readonly DataContext _context;
    private static readonly SequenceUtils sequenceUtils = new SequenceUtils();
    public SequenceService(DataContext context) {
        _context = context;
    }

    public ObservationResponse AddObservation(Guid Id, Observation observation)
    {
        var sequence = GetSequence(Id);
        if (sequence.Color.ToLower() == "red") {
            throw new Exception("The red observation should be the last");
        }
        if (sequence.ObservationCount == 0 && observation.Color.ToLower() == "red") {
            throw new Exception("There isn't enough data");
        }
        sequence.Color = observation.Color;
        if (observation.Color.ToLower() == "red") {
            var possibleNumbers = new List<int> { sequence.ObservationCount };
            sequence.Start = possibleNumbers.Intersect(sequence.Start).ToList();
        } else {
            if (observation.Numbers == null) {
                throw new Exception("Invalid numbers list");
            }
            List<int> numbers = sequenceUtils.StringsToIntegers(observation.Numbers);
            var possibleNumbers = sequenceUtils.GeneratePossibleNumbers(numbers, sequence.ObservationCount);
            sequence.Start = possibleNumbers.Intersect(sequence.Start).ToList();
            
            int andAllFirstDigits = sequence.Start.Aggregate(127, (acc, c) =>
            {
                int x = c - sequence.ObservationCount;
                int f = x / 10;
                return acc & sequenceUtils.digitEncoding[f];
            });

            int andAllSecondDigits = sequence.Start.Aggregate(127, (acc, c) =>
            {
                int x = c - sequence.ObservationCount;
                int s = x % 10;
                return acc & sequenceUtils.digitEncoding[s];
            });

            sequence.Missing[0] |= numbers[0] ^ andAllFirstDigits;
            sequence.Missing[1] |= numbers[1] ^ andAllSecondDigits;
        }

        sequence.ObservationCount += 1;
        ModifySequence(sequence);
        return new ObservationResponse(sequence.Start, sequenceUtils.IntegersToBinaryStrings(sequence.Missing));
    }

    public void Clear()
    {
        _context.Sequences.ExecuteDelete();
    }

    public void CreateSequence(SequenceModel sequence) 
    {
        var entity = new SequenceEntity
        {
            Id = sequence.Id,
            ObservationCount = sequence.ObservationCount,
            Start = sequence.Start,
            Missing = sequence.Missing,
            Color = sequence.Color
        };

        _context.Sequences.Add(entity);
        _context.SaveChanges();
    }

    public void ModifySequence(SequenceModel sequence) {
        var sequenceEntity = _context.Sequences.Find(sequence.Id);
        if (sequenceEntity is null)
        {
            throw new KeyNotFoundException("The sequence isn't found");
        }
        sequenceEntity.Color = sequence.Color;
        sequenceEntity.ObservationCount = sequence.ObservationCount;
        sequenceEntity.Start = sequence.Start;
        sequenceEntity.Missing = sequence.Missing;
        _context.SaveChanges();
    }

    public SequenceModel GetSequence(Guid id)
    {
        var sequence = _context.Sequences.Find(id);
        if (sequence is null)
        {
            throw new KeyNotFoundException("The sequence isn't found");
        }
        
        return new SequenceModel()
        {
            Id = sequence.Id,
            ObservationCount = sequence.ObservationCount,
            Start = sequence.Start,
            Missing = sequence.Missing,
            Color = sequence.Color
        };
    }

}