using System;
using NUnit.Framework;

namespace GameLogic.Tests
{
    public class MatchGameTests
    {
        private const int Milk = 0;
        private const int Cheese = 1;
        private const int Tomato = 2;

        private static MatchGame CreateGame()
        {
            return new MatchGame(new[] { Milk, Cheese, Milk, Tomato, Milk, Cheese }, Milk);
        }

        [Test]
        public void TryMove_OrderedItem_MovesFromShelfToBasket()
        {
            var game = CreateGame();

            var result = game.TryMove(0, out int basketSlot);

            Assert.AreEqual(MoveResult.MovedToBasket, result);
            Assert.AreEqual(0, basketSlot);
            Assert.IsFalse(game.Shelf.HasItem(0));
            CollectionAssert.AreEqual(new[] { Milk }, game.Basket.Items);
        }

        [Test]
        public void TryMove_ThreeOrderedItems_ClosesBasket()
        {
            var game = CreateGame();

            game.TryMove(0, out _);
            game.TryMove(2, out _);
            var result = game.TryMove(4, out _);

            Assert.AreEqual(MoveResult.BasketClosed, result);
            Assert.IsTrue(game.Basket.IsClosed);
        }

        [Test]
        public void TryMove_WrongItem_GoesToCounter()
        {
            var game = CreateGame();

            var result = game.TryMove(1, out int counterSlot);

            Assert.AreEqual(MoveResult.MovedToCounter, result);
            Assert.AreEqual(0, counterSlot);
            Assert.IsFalse(game.Shelf.HasItem(1));
            Assert.IsEmpty(game.Basket.Items);
            CollectionAssert.AreEqual(new[] { Cheese }, game.Counter.Items);
        }

        [Test]
        public void TryMove_ClosedBasket_Rejects()
        {
            var game = CreateGame();
            game.TryMove(0, out _);
            game.TryMove(2, out _);
            game.TryMove(4, out _);

            var result = game.TryMove(1, out _);

            Assert.AreEqual(MoveResult.Rejected, result);
            Assert.AreEqual(Cheese, game.Shelf.GetItem(1));
        }

        [Test]
        public void Constructor_OrderWithoutThreeOnShelf_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                new MatchGame(new[] { Milk, Milk, Cheese, Cheese, Tomato, Tomato }, Milk));
        }
    }
}
